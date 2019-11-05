<?php
require 'ConnectionSettings.php';

// User variables
$itemID = $_POST["itemID"];


// Checking connection
if($conn->connect_error) {
    die("Connection failed: ".$conn->connect_error);
}

// Query to get item from specific ID
$sql = "SELECT name, description, image FROM items WHERE id = '".$itemID."'";
$result = $conn->query($sql);

// If result is not empty:
if($result->num_rows > 0) {
    // Make an array
    $rows = array();
    while($row = $result->fetch_assoc()) {
        // Add item
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