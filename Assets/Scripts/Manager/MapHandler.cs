using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public Transform playerSpanwPoint;

    public GroundHandler groundHandler;

    /// <summary>
    /// Danh sách các điểm sinh enemy ngẫu nhiên
    /// Đánh số thứ tự từ dưới lên trên, từ trái sang phải
    /// Góc tọa độ phía trái dưới
    /// </summary>
    public List<Transform> enemySpawnPoints;

}
