
# React JS and .NET Core Image to Line-by-Line Game Algorithm

## Project Overview

This project involves creating an interactive web application using React JS and .NET Core. The application transforms a single-object image into a line-by-line game. Users can define the type of jumps between points, enabling experiential learning through the site. Users can explore sequences such as A-B, computer science series, and engineering sequences, all according to their preferences.

## Features

- **Image Processing**: Convert images to line-based representations.
- **Custom Jumps**: Allow users to define point-to-point jumps.
- **Educational Content**: Facilitate learning through interactive sequences.
- **Algorithm Visualization**: Illustrate algorithms used for point detection and boundary recognition.

## Algorithms

### 1. Moravec's Algorithm for Interest Point Detection

![image](https://github.com/shira050/KavLekav-server/assets/98688678/a6a6be14-04c5-4735-87f1-34e1ccb0c071)


Moravec's algorithm identifies points of interest in an image, which are not necessarily within the frame.

#### Steps:
1. **Identify Points**: Detect points that are significant based on their surroundings.
2. **Algorithm Process**: For each pixel, iterate over its neighbors and calculate the variance for each.
3. **Variance Map**: Generate a map based on the calculated variances.

![image](https://github.com/shira050/KavLekav-server/assets/98688678/2b063b6e-40d8-4027-b3ee-4eca889d6fe3)

#### Illustration:
- Points of interest are identified even if they are outside the primary focus of the image.

### 2. Boundary and Perimeter Point Detection
![image](https://github.com/shira050/KavLekav-server/assets/98688678/db154b33-dc42-466a-8fff-b8e694d3d97f)


This algorithm detects boundary points and the perimeter of the object in the image.

#### Steps:
1. **First Black Pixel**: Find the first black pixel.
2. **Neighbor Search**: For each neighbor, search for a black neighbor and recursively continue the search for \( n-1 \) from the previously found neighbor.
![image](https://github.com/shira050/KavLekav-server/assets/98688678/1b8a6b80-ef89-4634-8954-f67022ae96ce)
![image](https://github.com/shira050/KavLekav-server/assets/98688678/9ea790c9-89b5-42bb-b8ee-628e422fc851)

#### Neighbor Layout:
![image](https://github.com/shira050/KavLekav-server/assets/98688678/6a9edc37-638c-4a05-ae9c-55adace9f0a2)


### Usage Instructions

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/your-repo.git
    cd your-repo
    ```

2. **Backend Setup (.NET Core)**:
    - Ensure you have .NET Core SDK installed.
    - Navigate to the backend project folder and run:
      ```bash
      dotnet restore
      dotnet build
      dotnet run
      ```

3. **Frontend Setup (React JS)**:
    - Ensure you have Node.js and npm installed.
    - Navigate to the frontend project folder and run:
      ```bash
      npm install
      npm start
      ```

4. **Access the Application**:
    - Open your browser and go to `http://localhost:3000` to access the frontend.
    - The backend should be running on `http://localhost:5000` by default.

### Example

Include illustrative images and examples demonstrating the algorithm:
![image](https://github.com/shira050/KavLekav-server/assets/98688678/aa1ef073-b0d1-4438-85b0-42dba3681777)


### Contributions

We welcome contributions! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

### License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Feel free to customize the README file further to match the specifics of your project, such as adding images, links, or more detailed instructions.
