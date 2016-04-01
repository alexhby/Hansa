
<?php
require 'vendor/autoload.php';
include_once 'functions.php';

$app = new \Slim\Slim();

$body = $app->request->getBody();
$body1 = json_decode($body);
$user = $body1->{"username"};
$epwd = $body1->{"password"};


$servername = "localhost";
$username = "root";
$password = "260562995";
$dbname = "hansa361";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$id=0;
$uname="";
$pword="";
//$sharedkey = 12;
$exists = false; 

$sub;
$acc;
$dpword = $epwd;//decrypt($epwd,$sharedkey,"");



$sql = "SELECT * FROM users WHERE username='$user' AND password='$dpword'";
$result = $conn->query($sql);
$success="...";




if ($result->num_rows > 0) {
    // output data of each row
    if($row = $result->fetch_assoc()) {
    	global $uname, $pword;
    	
    	$uname = $row["username"];
    	
    	$pword = $row["password"];
        $success =  "your user EXISTS!";
        $exists = true;

    }
} else {
    $success = "0 results";
}


if($dpword == $pword && $pword != "" && $uname != ""){
	//$_{POST} =logg
	logsuccess($uname);
}
else{
	logfail();
}

function decrypt($str,$sk,$acc){
	for ($i=0; $i < strlen($str); $i++) { 
		$sub = ord($str[$i]) - $sk - $i;
		if ($sub <33){
			$sub = $sub + 94;
		}
		$acc = $acc.chr($sub);	
	}
	return $acc;
}

function logsuccess($u){

	//CONNECT TO DB AND GET PLAYER INFO
	
	

}

function logfail(){
	//RETURN ERROR!!!!

	echo "1";
	
}




$conn->close();
?>

