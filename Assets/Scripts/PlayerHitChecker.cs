using System;
using UnityEngine;
using UnityEngine.Events;

public enum BeatHitType
{
    Attack,
    Jump,
    HeavyAttack
}
public class PlayerHitChecker : MonoBehaviour
{
    public float hitDistance = 0.5f; // tweak this for leniency
    public LayerMask obstacleLayer;  // put obstacles on this layer

    [SerializeField] public UnityEvent onAttackSuccess, onJumpSuccess, onHeavyAttackSuccess;

    private void OnEnable()
    {
        GetComponent<Player>().onActionPerformed += CheckHit;
    }

    private void OnDisable()
    {
        GetComponent<Player>().onActionPerformed -= CheckHit;
    }

    public void CheckHit(BeatHitType type)
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
            if(type != closest.GetComponent<Obstacle>().type)
                closest.GetComponent<Obstacle>().ReactToPlayerInteraction(ObstacleSuccessState.Fail);
            else
            {
                closest.GetComponent<Obstacle>().ReactToPlayerInteraction(ObstacleSuccessState.Success);
                switch (type)
                {
                    case BeatHitType.Attack:
                        onAttackSuccess?.Invoke();
                        break;
                    case BeatHitType.HeavyAttack:
                        onHeavyAttackSuccess?.Invoke();
                        break;
                    case BeatHitType.Jump:
                        onJumpSuccess?.Invoke();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
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