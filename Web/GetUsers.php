<?php
require 'ConnectionSettings.php';


// Checking connection
if($conn->connect_error) {
    die("Connection failed: ".$conn->connect_error);
}

// Query to select user information
$sql = "SELECT id, username, password FROM users";
$result = $conn->query($sql);

// If result is not empty:
if($result->num_rows > 0) {
    // Output data of each row
    while($row = $result->fetch_assoc()) {
        // Showing id, username and password
        echo "id: ".$row['id']." name: ".$row['username']." password: ".$row['password']."<br>";
    }
}
else {
    echo "No users found.";
}

$conn->close();

?>