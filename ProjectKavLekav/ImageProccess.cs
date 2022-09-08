using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKavLekav
{
   public class ImageProccess
    {
        const Int32 avg = 200;
        const Int32 FF = 255;
        const Int32 zero = 0;

        const double Red = .299;
        const double Green = .587;
        const double Blue = .114;
        List<Point> lstCorners = new List<Point>();//רשימה של נקודות


        List<Point> lstBorder = new List<Point>();//רשימה של נקודות גבול
        List<Point> lstPoints = new List<Point>();
        List<string> lstSign = new List<string>();//רשימת הסימונים על הנקודות למספור
        public Bitmap Picture { get; set; }
        int[,] tempPicture;
        public ImageProccess(Bitmap Picture)
        {
            this.Picture = Picture;
        }
   



        public Bitmap CreatePoints(int c,int a1,int d)
        {
            ToGray();
            Picture = PerformClean(Picture);
            int countRKC = Region();
            if (countRKC > 1)
            {
                Picture = null;
            }
            else
            {
                find_corners();//נקודות עניין
                ToBlackWhite();
                FindBorder();//מציאת גבול תמונה//
                lstCorners = ReduceList(lstCorners);//דילול
                lstBorder = ReduceList(lstBorder);
                lstPoints = intersectLists(lstBorder, lstCorners);//איחוד
                //if (lstBorder.Count == 0)
                //{
                //    lstPoints = lstCorners;
                //    for (int i = 0; i < lstPoints.Count; i++) {lstSign.Add("");}
                //}

               // else if (lstCorners.Count == 0)
                //    lstPoints = lstBorder;
               
                switch (c)
                {
                    case 1:
                        fillSeries(1, 1);
                        break;
                    case 2:
                        fillLetters('א');
                        break;
                    case 3:
                        fillLetters('a');
                        break;
                    case 4:
                        fillSeries(a1, d);
                        break;
                    case 5:
                        fillEngineeringSeries(a1, d, '*');
                        break;
                    case 6:
                        fillEngineeringSeries(a1, d, '/');
                        break;
                }
                Picture = countingMap(lstSign);
            }
            return Picture;

        }


        public void ToGray()//הופך תמונה לאפור
        {
            Bitmap picMap = (Bitmap)Picture.Clone();
            Color c;

            for (int i = 0; i < picMap.Width; i++)
            {
                for (int j = 0; j < picMap.Height; j++)
                {
                    c = picMap.GetPixel(i, j);
                    byte gray = (byte)(Red * c.R + Green * c.G + Blue * c.B);
                    picMap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            Picture = (Bitmap)picMap.Clone();
        }
        public void ToBlackWhite()//הופך תמונה לשחור לבן
        {
            Bitmap picMap = Picture;
            Color newColor;

            for (int i = 0; i < picMap.Width; i++)
            {
                for (int j = 0; j < picMap.Height; j++)
                {
                    Color pixelColor = picMap.GetPixel(i, j);
                    if (pixelColor.R <= avg && pixelColor.G <= avg && pixelColor.B <= avg)
                        newColor = Color.FromArgb(zero, zero, zero);//שחור
                    else
                        newColor = Color.FromArgb(FF, FF, FF);//לבן
                    picMap.SetPixel(i, j, newColor);
                }
            }
            Picture = picMap;

        }
        public double[,] ToDouble(Bitmap image)
        {
            //////ממיר תמונה ל double
            var r = new double[image.Width, image.Height];
            var g = new double[image.Width, image.Height];
            var b = new double[image.Width, image.Height];
            var a = new double[image.Width, image.Height];
            double[,] res = new double[image.Width, image.Height];
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            unsafe
            {
                BitmapData bitmapData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

                int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                //if (bytesPerPixel == 3)
                //{
                //    for (int y = 0; y < heightInPixels; y++)
                //    {
                //        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                //        for (int x = 1; x <= widthInBytes; x = x + bytesPerPixel)
                //        {
                //            r[(x - 1) / bytesPerPixel, y] = (double)currentLine[x + 1] / 255;
                //            g[(x - 1) / bytesPerPixel, y] = (double)currentLine[x] / 255;
                //            b[(x - 1) / bytesPerPixel, y] = (double)currentLine[x - 1] / 255;
                //            a[(x - 1) / bytesPerPixel, y] = 0.0d;
                //        }
                //    }
                //}
                //else
                //{
                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                        for (int x = 1; x <= widthInBytes; x = x + bytesPerPixel)
                        {
                                r[(x - 1) / bytesPerPixel, y] = (double)currentLine[x + 1] / 255;
                            g[(x - 1) / bytesPerPixel, y] = (double)currentLine[x] / 255;
                            b[(x - 1) / bytesPerPixel, y] = (double)currentLine[x - 1] / 255;
                            if (bytesPerPixel == 3)
                            {
                                a[(x - 1) / bytesPerPixel, y] = 0.0d;
                            }
                            else
                                a[(x - 1) / bytesPerPixel, y] = (double)currentLine[x + 2] / 255;
                        }
                    //}
                }
                image.UnlockBits(bitmapData);
            }

            //watch.Stop();

            //Console.WriteLine("Milliseconds: {0}: ", watch.ElapsedMilliseconds);

            for (int i = 0; i < res.GetLength(0); i++)
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    res[i, j] = Red * r[i, j] + Green * g[i, j] + Blue * b[i, j];
                }

            return res;
        }




        public void FindBorder()
        {

            int x, y = 0, b = 0;
            Point start = new Point(0, 0);//התחלה 
            int step = 1;
            lstBorder.Clear();//מנקה את הרשיימה
            int[,] mat = new int[2, 8];
            mat[0, 0] = -1;
            mat[0, 1] = -1;
            mat[0, 2] = 0;
            mat[0, 3] = -1;
            mat[0, 4] = 0;
            mat[0, 5] = 1;
            mat[0, 6] = 1;
            mat[0, 7] = 1;
            mat[1, 0] = 0;
            mat[1, 1] = -1;
            mat[1, 2] = -1;
            mat[1, 3] = 1;
            mat[1, 4] = 1;
            mat[1, 5] = 1;
            mat[1, 6] = 0;
            mat[1, 7] = -1;
            //מציאת נקודת ההתחלה - הפיקסל השחור הראשון
            for (x = 0; x < Picture.Width; x++)
            {
                for (y = 0; y < Picture.Height; y++)
                    if (Picture.GetPixel(x, y) == Color.FromArgb(zero, zero, zero))//אם הנקודה שחורה
                    {
                        start = new Point(x, y);//אז תתחיל מהנקודה הזו
                        break;//ותעצור
                    }
                if (y < Picture.Height)//אם נעצרת באמצע
                    break;//תעצור גם פה
            }
            //מעבר על כל השכנים החל מהשכן המערבי עד למציאת פיקסל שחור
            do
            {
                int i = 0;
                for (i = 0; i < 8; i++)
                {
                    if (x + mat[0, b] * step >= 0 && x + mat[0, b] * step < Picture.Width && y + mat[1, b] * step >= 0 && y + mat[1, b] * step < Picture.Height)//תקינות -שאין יציאה מהגבולות
                        if (Picture.GetPixel(x + mat[0, b] * step, y + mat[1, b] * step) == Color.FromArgb(zero, zero, zero))//וגם אם אתה שחור
                        {


                            lstBorder.Add(new Point(x + mat[0, b] * step, y + mat[1, b] * step));//תוסיף לי לרשימה את הנקודה
                           // Picture.SetPixel(x + mat[0, b] * step, y + mat[1, b] * step, Color.Blue);
                            x = x + mat[0, b] * step;
                            y = y + mat[1, b] * step;
                            b = b - 1;//ממה שמצאתי n-1  עבור לשכן ה
                            if (b < 0)
                                b = 7;
                            step = 1;//איתחול הצעד מחדש
                            break;

                        }
                        else
                        {
                            b++;
                            if (b == 8)
                                b = 0;
                        }
                }
                if (i == 8)// אם אין לי שכן שחור אז תחפש בדילוג של פיקסל
                {
                    step++;
                }

            }

            while (x != start.X || y != start.Y);


        }

        public List<Point> ReduceList(List<Point> list)//פונקצית דילול
        {
            List<Point> lst = new List<Point>();
            while (list.Count > 0)
            {
                Point p = new Point(list[0].X, list[0].Y);
                list.RemoveAll(p1 => FindDistance(p, p1) < 30);
                lst.Add(p);
            }
            return lst;
        }



        public double FindDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public List<Point> intersectLists(List<Point> lst1, List<Point> lst2)//מאחד רק את 2 הרשימות בנקודות המשותפות בין גבול והמרכזיות
        {
            List<Point> lst = new List<Point>();
            
            foreach (Point point in lst1)
            {
               
                foreach (Point point1 in lst2)
                    if (FindDistance(point, point1) < 150)
                    {
                        lst.Add(point);
                        break;
                    }
            }
            return lst;
        }


        public void find_corners()
        {

            double[,] mat = ToDouble(Picture);
            double max = 0;
            double[,] mat2 = new double[mat.GetLength(0), mat.GetLength(1)];
            for (int i = 2; i < mat.GetLength(0) - 8; i++)
             for (int j = 2; j < mat.GetLength(1) - 8; j++)
                    mat2[i, j] = calculate_point(mat, i, j, 7);
            //מציאת מקסימום
            for (int i = 2; i < mat2.GetLength(0) - 8; i++)
                for (int j = 2; j < mat2.GetLength(1) - 8; j++)
                    if (mat2[i, j] > max)
                        max = mat2[i, j];
            int cnt = 0;

            //של השונוית המינימאליות מערך מונים
            int[] arr = new int[(int)max + 1];
            for (int i = 2; i < mat2.GetLength(0) - 10; i++)
                for (int j = 2; j < mat2.GetLength(1) - 10; j++)
                {
                    arr[(int)(mat2[i, j])]++;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                }
           //לוקח את השונויות המאקסימאליות באופן יחסי
           //לוקח לא רק את השונות שהיא מקסימום בלבד אלא גם את הסביבה שלה
           //כל עוד זה בטווח של אחד חלקי אלף מהנקודות ולא יותר
            double sum = 0, border = max;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                sum += arr[i];
                if (sum / (mat2.GetLength(0) * mat2.GetLength(1)) > 0.001)
                {
                    border = i;
                    break;
                }
            }
            for (int i = 2; i < mat2.GetLength(0) - 10; i++)
                for (int j = 2; j < mat2.GetLength(1) - 10; j++)
                {
                    if (mat2[i, j] >= border)//הנקודות למיספור
                    {
                        lstCorners.Add(new Point(i, j));//תוסיף לי לרשימה את הנקודה

                        cnt++;
                        //Picture.SetPixel(i, j, Color.Red);

                    }

                    //else
                    //    Picture.SetPixel(i, j, Color.White);
                }


        }
        //עוברת על כל השכנים ועבור כל אחד מחשבת שונות
        public double calculate_point(double[,] mat, int x, int y, int size)
        {
            double val, min = 10;
            for (int i = x - 1, cnt = 0; cnt < 3; i++, cnt++)
                for (int j = y - 1, cnt2 = 0; cnt2 < 3; j++, cnt2++)
                {
                    if (i != x || j != y)
                    {
                        val = calculate(mat, x, y, i, j, size);
                        if (val < min)
                            min = val;
                    }
                }
            return min;
        }

        //פונקציה המקבלת קורדינציות של פינה שמאלית עליונה של 
        //שתי מטריצות ומחזירה את
        //סכום ריבוע הפרשי
        //הערכים בין נקודות תואמות במטריצה, הפונקציה מקבלת גם את גודל החלון

        public double calculate(double[,] mat, int x1, int y1, int x2, int y2, int size)
        {
            double sum = 0;
            for (int i = x1, k = x2; i < x1 + size; i++, k++)
                for (int j = y1, l = y2; j < y1 + size; j++, l++)
                    sum += Math.Pow(mat[i, j] - mat[k, l], 2);
            return sum;
        }


        public void fillLetters(char a1)//מילוי אותיות עברית או אנגלית
        {
            char cnt = a1;
            bool isEnglish = a1 == 'a' ? true : false;
            for (int i = 0; i < lstPoints.Count; i++)
            {
                if (isEnglish && cnt > 'z')
                    cnt = a1;
                else if (!isEnglish && cnt > 'ת')
                    cnt = a1;

                lstSign.Add(cnt.ToString());
                cnt++;
            }
        }
        public void fillSeries(int a1, int d)//סדרות חשבוניות
        {
            int cnt = a1;
            for (int i = 0; i < lstPoints.Count; i++)
            {
                lstSign.Add(cnt.ToString());

                cnt += d;
            }

        }
        public void fillEngineeringSeries(int a1, int d, char op)//סדרות כפל או חילוק
        {
            int cnt = a1;
            for (int i = 0; i < lstPoints.Count; i++)
            {
                lstSign.Add(cnt.ToString());
                if (cnt >= 100 || cnt <= 0)
                    cnt = a1;


                switch (op)
                {
                    case '*':
                        cnt *= d; break;
                    case '/':
                        cnt /= d; break;
                }
            }

        }

        //מספור תמונה
        public Bitmap countingMap(List<string> lstSign)
        {

            Bitmap bitmap = new Bitmap(Picture.Width, Picture.Height, Picture.PixelFormat);

            Graphics graphicsImage = Graphics.FromImage(bitmap);



            StringFormat stringformat2 = new StringFormat();
            stringformat2.Alignment = StringAlignment.Center;
            stringformat2.LineAlignment = StringAlignment.Center;


            Color StringColor2 = System.Drawing.ColorTranslator.FromHtml("red");
            string Str_TextOnImage2 = lstSign[0];



            graphicsImage.DrawString(Str_TextOnImage2, new Font("Arial", 10,
            FontStyle.Bold), new SolidBrush(StringColor2), new Point(lstPoints[0].X - 10, lstPoints[0].Y),
            stringformat2);
            graphicsImage.DrawString(
                ((char)183).ToString(), new Font("Arial", 8,
           FontStyle.Regular), new SolidBrush(StringColor2), new Point(lstPoints[0].X, lstPoints[0].Y),
           stringformat2);


            int index = 0;
            int x = find_next_point(0);

            while (x != 0)
            {
                index++;
                for (int i = lstPoints[x].X; i < bitmap.Height && i < lstPoints[x].X + 5; i++)
                    for (int j = lstPoints[x].Y; j < lstPoints[x].Y + 5 && j < bitmap.Width; j++)
                        graphicsImage = Graphics.FromImage(bitmap);



                stringformat2 = new StringFormat();
                stringformat2.Alignment = StringAlignment.Center;
                stringformat2.LineAlignment = StringAlignment.Center;


                StringColor2 = System.Drawing.ColorTranslator.FromHtml("#000000");

                Str_TextOnImage2 = lstSign[index];



                graphicsImage.DrawString(Str_TextOnImage2, new Font("david", 8,
                FontStyle.Regular), new SolidBrush(StringColor2), new Point(lstPoints[x].X - 10, lstPoints[x].Y),
                stringformat2);


                graphicsImage.DrawString(
                ((char)183).ToString(), new Font("Arial", 6,
           FontStyle.Regular), new SolidBrush(StringColor2), new Point(lstPoints[x].X, lstPoints[x].Y),
           stringformat2);
                x = find_next_point(x);
            }
            lstSign.Clear();
            return (Bitmap)bitmap.Clone();



        }


        public int find_next_point(int index)
        {
            if (index == lstPoints.Count - 1)
                return 0;
            return index + 1;

        }


        public int Region()
        {
            int y = 0;
            tempPicture = new int[Picture.Width, Picture.Height];
          

            DisjointSet ds = new DisjointSet(1000);
            List<int>[] arr = new List<int>[1000];
            for (int i = 0; i < 1000; i++)
            {
                arr[i] = new List<int>();
            }
            string s = "";
          
            int tav = 0;
            string str = "";
            for (int x = 0; x < tempPicture.GetLength(0); x++)
            {

                for (y = 0; y < tempPicture.GetLength(1); y++)
                {


                    //אם הפיקסל בתמונה המקורית הוא שחור 
                    //  if (Picture.GetPixel(x, y) == Color.FromArgb(zero, zero, zero)) //וגם אם אתה שחור
                    Color pixelColor = Picture.GetPixel(x, y);

                    if (pixelColor.R <= avg && pixelColor.G <= avg && pixelColor.B <= avg)
                    {
                        
                        int befor = 0, on = 0;

                        if (x > 0)
                            on = tempPicture[x - 1, y];
                        if (y > 0)
                            befor = tempPicture[x, y - 1];

                        if (befor == 0 && on == 0)//ל2 השכנים אין תווית עדיין
                        {
                            tempPicture[x, y] = ++tav;//תווית חדשה


                        }
                        else
                        {
                            //אם לשני השכנים הנ"ל אותה תווית, העתק אותה

                            if (befor == on)
                                tempPicture[x, y] = on;
                            //. אם רק לשכן השמאלי או לשכן שמעל יש תווית, העתק אותה.
                            else
                            if ((befor != 0 && on == 0) || (on != 0 && befor == 0))
                            {
                                if (befor != 0)
                                    tempPicture[x, y] = befor;
                                else
                                    tempPicture[x, y] = on;
                            }

                            //אם לשניהם תוויות שונות, העתק את זו של השכן מעל וסמן את התוויות כשקולות 
                            // בטבלת השקילות.
                            else
                            if (befor != on)
                            {
                                tempPicture[x, y] = on;
                             
                                ds.union(befor, on);
                            }
                        }



                    }
                }
            }
            int[] a = new int[1000];

            //מצא את התווית בעלת הערך קטן ביותר לכל קבוצת שקילות
            for (int i = 0; i < tempPicture.GetLength(0); i++)
                for (int j = 0; j < tempPicture.GetLength(1); j++)
                {
                    if (tempPicture[i, j] != 0)
                        tempPicture[i, j] = ds.find(tempPicture[i, j]);

                    a[ds.find(tempPicture[i, j])]++;//מערך מונים על הנציגים
                }
            List<int> rkc = new List<int>();
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] != 0)
                    rkc.Add(i);
            }

            return rkc.Count;



        }









        public Bitmap PerformClean(Bitmap bitmap)
        {
            var newBitmap = new UnsafeBitmap(bitmap);
            newBitmap.LockBitmap();


            PixelData color;
            var mask = new int[9];
            for (var i = 0; i < 9; i++)
                mask[i] = 1;

            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        color = newBitmap.GetPixel(i - 1, j - 1);
                        mask[0] = Convert.ToInt16(color.GetBrightness() * 255);
                    }
                    else
                        mask[0] = 255;

                    if (j - 1 >= 0 && i + 1 < bitmap.Width)
                    {
                        color = newBitmap.GetPixel(i + 1, j - 1);
                        mask[1] = Convert.ToInt16(color.GetBrightness() * 255);
                    }
                    else
                        mask[1] = 255;

                    if (j - 1 >= 0)
                    {
                        color = newBitmap.GetPixel(i, j - 1);
                        mask[2] = Convert.ToInt16(color.GetBrightness() * 255);
                    }
                    else
                        mask[2] = 255;

                    if (i + 1 < bitmap.Width)
                    {
                        color = newBitmap.GetPixel(i + 1, j);
                        mask[3] = Convert.ToInt16(color.GetBrightness() * 255);
                    }
                    else
                        mask[3] = 255;

                    if (i - 1 >= 0)
                    {
                        color = newBitmap.GetPixel(i - 1, j);
                        mask[4] = Convert.ToInt16(color.GetBrightness() * 255);
                    }
                    else
                        mask[4] = 255;

                    if (i - 1 >= 0 && j + 1 < bitmap.Height)
                    {
                        color = newBitmap.GetPixel(i - 1, j + 1);
                        mask[5] = Convert.ToInt16(color.GetBrightness() * 255);
                    }
                    else
                        mask[5] = 255;

                    if (j + 1 < bitmap.Height)
                    {
                        color = newBitmap.GetPixel(i, j + 1);
                        mask[6] = Convert.ToInt16(color.GetBrightness() * 255);
                    }
                    else
                        mask[6] = 255;

                    if (i + 1 < bitmap.Width && j + 1 < bitmap.Height)
                    {
                        color = newBitmap.GetPixel(i + 1, j + 1);
                        mask[7] = Convert.ToInt16(color.GetBrightness() * 255);
                    }
                    else
                        mask[7] = 255;

                    color = newBitmap.GetPixel(i, j);
                    mask[8] = Convert.ToInt16(color.GetBrightness() * 255);

                    Array.Sort(mask);
                    var mid = mask[4];
                    newBitmap.SetPixel(i, j, new PixelData { B = (byte)mid, R = (byte)mid, G = (byte)mid });
                }
            }
            newBitmap.UnlockBitmap();
            return newBitmap.Bitmap;
        }




    }
}
