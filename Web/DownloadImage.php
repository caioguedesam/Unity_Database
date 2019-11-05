<?php

require 'ConnectionSettings.php';

// Checking connection
if($conn->connect_error) {
    die("Connection failed: ".$conn->connect_error);
}

if($_POST) {
    if(isset($_POST["imageURL"])) {
        $imageURL = $_POST["imageURL"];
        $path = "images/" . $imageURL . ".png";

        if(file_exists($path)) {
            $imgEncode = file_get_contents($path);
            echo $imgEncode;
        }
        else {
            echo "File not found.";
        }
    }
}
else {
    $imageURL = "default";
    $path = "images/" . $imageURL . ".png";

    if(file_exists($path)) {
        $imgEncode = file_get_contents($path);
        echo $imgEncode;
    }
    else {
        echo "File not found.";
    }
}


?>