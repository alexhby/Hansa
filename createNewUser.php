
<?php



$servername = "hansa361.c8rnmv72to33.us-east-1.rds.amazonaws.com:3306:3306";
$username = "alexhe";
$password = "hansa361team";
$dbname = "hansa361";

// hansa361 connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

//$sql = "SELECT * FROM members WHERE username='".$user."'";
//$result = $conn->query($sql);
//$success="...";

$id;
/*$uname=$_POST["username"];
$pword=$_POST["password"];*/
$uname="tommysssss";
$pword="tester";




$exist = "SELECT * FROM players WHERE username = '$uname' AND password = '$pword'";
$res = $conn->query($exist);
echo json_encode($res);
if(is_null($res->num_rows )){
	logfail();
}
else{
	//create new user in the database
	logsuccess($uname, $pword);
}



function logsuccess($user, $pword){
	global $conn;
	
	$q = "INSERT INTO players ( username, password) VALUES ( '$user', '$pword');";
	$conn->query($q);

	echo "0";
	
}

function logfail(){
	//a user with that exact profile already exists
	echo "1";
	
}

$conn->close();
?>

