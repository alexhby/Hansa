
var nodeRadius : float;
var unwalkableMask : LayerMask;
var gridWorldSize : Vector2;
var player : Transform;

private var nodeDiameter : float;
private var gridSizeX : int;
private var gridSizeY : int;
private var grid : Array; //2D array

function createArray(dimensions : Array) : Array{
    
	var r = dimensions[0];
	var c = dimensions[1];
    var newArray = new Array();
    for (var i = 0; i < r; i++) {
    	newArray[i] = new Array(c);
	}
    return newArray;
};

function Start(){
	nodeDiameter = nodeRadius*2;
	gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
	gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
	createGrid();
	
};

function createGrid(){
	grid = createArray([gridSizeX,gridSizeY]);
	var worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;
	for (var x : int = 0; x < gridSizeX; x++){
		for (var y : int = 0; y < gridSizeY; y++){
			var worldPoint : Vector3 = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
			var walkable : boolean = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
			grid[x][y] = new Node(walkable,worldPoint);
		}
	}
};

function findNodeOfPlayer( pos : Vector3 ){
	var percentX : float = ( pos.x + gridWorldSize.x/2 ) / gridWorldSize.x;
	var percentY : float = ( pos.z + gridWorldSize.y/2 ) / gridWorldSize.y;
	percentX = Mathf.Clamp01(percentX);
	percentY = Mathf.Clamp01(percentY);
	
	var x : int = Mathf.RoundToInt((gridSizeX - 1) * percentX);
	var y : int = Mathf.RoundToInt((gridSizeY - 1) * percentY);
	
	return grid[x][y];
}
	
function OnDrawGizmos(){
	Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));
	
	if ( grid != null){
		var playerNode : Node = findNodeOfPlayer(player.position);
		
		for ( var r : int = 0; r < gridSizeX ; r++){
			for ( var c : int = 0; c < gridSizeY; c++ ){ 
			
				if ( playerNode == grid[r][c] ){
					Gizmos.color = Color.blue; 
				} else if ( grid[r][c].walkable ){
					Gizmos.color = Color.white;
				} else { 
					Gizmos.color = Color.red; 
				}
				
				Gizmos.DrawCube( grid[r][c].worldPosition, new Vector3(1,0.1,1) * ( nodeDiameter - 0.1 ));
			}
		}
	}
};
	