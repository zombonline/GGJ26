using UnityEngine;

public class PlayerHitChecker : MonoBehaviour
{
    public float hitDistance = 0.5f; // tweak this for leniency
    public LayerMask obstacleLayer;  // put obstacles on this layer

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CheckHit();
        }
    }

    void CheckHit()
    {
        // find all obstacles in a small radius around the player
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitDistance, obstacleLayer);

        if (hits.Length > 0)
        {
            // take the closest one
            Collider2D closest = hits[0];
            float closestDist = Vector2.Distance(transform.position, closest.transform.position);

            for (int i = 1; i < hits.Length; i++)
            {
                float d = Vector2.Distance(transform.position, hits[i].transform.position);
                if (d < closestDist)
                {
                    closest = hits[i];
                    closestDist = d;
                }
            }

            Debug.Log("Success!");

            // disable or consume the obstacle
            closest.GetComponent<Obstacle>().ReactToPlayerInteraction(BeatHitType.Success);
        }
        else
        {
            Debug.Log("Miss");

        }
    }

    // just to see the hit area in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hitDistance);
    }
}