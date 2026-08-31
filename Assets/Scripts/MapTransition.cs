using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundary;

    private CinemachineConfiner2D confiner;

    [SerializeField] Direction direction;
    [SerializeField] float distance = 2f;

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private void Awake()
    {
        confiner = FindAnyObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            confiner.InvalidateBoundingShapeCache();

            SaveManager.Instance.currentBoundaryName = mapBoundary.gameObject.name;

            UpdatePlayerPos(collision.gameObject);
        }
    }

    private void UpdatePlayerPos(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += distance;
                break;

            case Direction.Down:
                newPos.y -= distance;
                break;

            case Direction.Left:
                newPos.x -= distance;
                break;

            case Direction.Right:
                newPos.x += distance;
                break;
        }

        player.transform.position = newPos;
    }

    public static void RestoreCameraBoundary()
    {
        var confiner = FindAnyObjectByType<CinemachineConfiner2D>();

        if (confiner == null)
            return;

        GameObject boundary =
            GameObject.Find(SaveManager.Instance.currentBoundaryName);

        if (boundary != null)
        {
            PolygonCollider2D collider =
                boundary.GetComponent<PolygonCollider2D>();

            if (collider != null)
            {
                confiner.BoundingShape2D = collider;
                confiner.InvalidateBoundingShapeCache();
            }
        }
    }
}