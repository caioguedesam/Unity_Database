<?php
require 'ConnectionSettings.php';

// User variables
$userID = $_POST["userID"];


// Checking connection
if($conn->connect_error) {
    die("Connection failed: ".$conn->connect_error);
}

// Query to get IDs of certain user's items
$sql = "SELECT itemID FROM usersitems WHERE userID = '".$userID."'";
$result = $conn->query($sql);

// If result is not empty:
if($result->num_rows > 0) {
    // Make an array for all items
    $rows = array();
    while($row = $result->fetch_assoc()) {
        // Add each item
        $rows[] = $row;
    }
    // Encode to JSON
    echo json_encode($rows);
}
else {
    echo "0";
}

$conn->close();

?>