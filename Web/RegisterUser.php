<?php
require 'ConnectionSettings.php';

// User variables
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Checking connection
if($conn->connect_error) {
    die("Connection failed: ".$conn->connect_error);
}

$sql = "SELECT username FROM users WHERE username = '".$loginUser."'";
$result = $conn->query($sql);

if($result->num_rows > 0) {
    echo "User ".$loginUser." already exists!";
}
else {
    $create_user_query = "INSERT INTO users (username, password) VALUES ('".$loginUser."', '".$loginPass."')";
    
    if($conn->query($create_user_query)) {
        echo "New user created!";
    }
    else {
        echo "Error: ".$create_user_query."<br>".$conn->error;
    }
}

$conn->close();

?>