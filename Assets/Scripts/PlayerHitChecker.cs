using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum BeatHitType
{
    Attack,
    Jump,
    HeavyAttack
}
public class PlayerHitChecker : MonoBehaviour
{
    [FormerlySerializedAs("hitDistance")] public float overlapDistance = 1f; // tweak this for leniency
    public float perfectDistance = 0.5f;
    public LayerMask obstacleLayer;  // put obstacles on this layer

    [SerializeField] public UnityEvent onAttackSuccess, onJumpSuccess, onHeavyAttackSuccess;
    [SerializeField] public UnityEvent onAttackFail, onJumpFail, onHeavyAttackFail;

    private float inputTime;
    private BeatHitType inputType;
    private void OnEnable()
    {
        GetComponent<Player>().onActionPerformed += ExecuteBeatHit;
    }

    private void OnDisable()
    {
        GetComponent<Player>().onActionPerformed -= ExecuteBeatHit;
    }

    private void Update()
    {
        if (inputTime + 0.05f < Time.time)
        {
            CheckHit(inputType);
            inputTime = float.PositiveInfinity;
        }
    }

    public void ExecuteBeatHit(BeatHitType type)
    {
        inputTime = Time.time;
        inputType = type;
    }
    

    public void CheckHit(BeatHitType type)
    {
        // find all obstacles in a small radius around the player
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, overlapDistance, obstacleLayer);

        if (hits.Length > 0)
        {
            // take the closest one
            Collider2D closest = hits[0];
            float closestDist = Vector2.Distance(transform.position, closest.transform.position);

            for (int i = 1; i < hits.Length; i++)
            {
                float d = Vector2.Distance(transform.position, hits[i].transform.position);
                if(!hits[i].GetComponent<Obstacle>().canHit)
                    continue;
                if (d < closestDist)
                {
                    closest = hits[i];
                    closestDist = d;
                }
            }
            bool perfect = closestDist < perfectDistance;
            if (type != closest.GetComponent<Obstacle>().type || !perfect)
            {
                switch (type)
                {
                    case BeatHitType.Attack:
                        onAttackFail?.Invoke();
                        break;
                    case BeatHitType.HeavyAttack:
                        onHeavyAttackFail?.Invoke();
                        break;
                    case BeatHitType.Jump:
                        onJumpFail?.Invoke();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
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
        Gizmos.DrawWireSphere(transform.position, overlapDistance);
    }
}