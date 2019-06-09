using UnityEngine;

public class GridData
{
    public Vector3 position;
    public GRID_DETAILS gridDetails;

    public GridData(Vector3 position, GRID_DETAILS gridDetails)
    {
        this.position = position;
        this.gridDetails = gridDetails;
    }
}
