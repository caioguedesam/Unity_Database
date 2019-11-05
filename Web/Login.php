<?php
require 'ConnectionSettings.php';

// User variables
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];


// Checking connection
if($conn->connect_error) {
    die("Connection failed: ".$conn->connect_error);
}

// Query to get login user's password and id
$sql = "SELECT password, id FROM users WHERE username = '".$loginUser."'";
$result = $conn->query($sql);

// If result is not empty:
if($result->num_rows > 0) {
    // Check for correct password
    while($row = $result->fetch_assoc()) {
       if($row["password"] == $loginPass) {
           // Print ID if correct
           echo $row["id"];
       }
       else {
           echo "Wrong credentials.";
       }
    }
}
else {
    echo "User not found.";
}

$conn->close();

?>