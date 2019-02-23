// Date   : 23.02.2019 14:06
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

public class BurnCaster : MonoBehaviour
{

    private PlayerConfig playerConfig;
    private GameConfig gameConfig;
    private FireConfig fireConfig;
    private PlayerHandConfig playerHandConfig;
    private FireSourceManager fireSource;

    private Burnable targetBurnable;
    private BurnableCollider previouslyHitBurnable;

    void Start()
    {
        playerConfig = ConfigManager.main.GetConfig("PlayerConfig") as PlayerConfig;
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        fireConfig = ConfigManager.main.GetConfig("FireConfig") as FireConfig;
        playerHandConfig = ConfigManager.main.GetConfig("PlayerHandConfig") as PlayerHandConfig;
    }

    void Update()
    {
        CheckBurnablePossibilities();
        ProcessInput();
    }

    void ProcessInput()
    {
        if (KeyManager.main.GetKeyDown(PlayerAction.ExtinguishOrLightFire))
        {
            BurnTarget();
        }
    }

    void BurnTarget()
    {
        if (targetBurnable != null && playerHandConfig.hasFire)
        {
            if (!targetBurnable.IsLit)
            {
                targetBurnable.Burn();
                playerHandConfig.triggerThrow = true;
            }
        }
    }

    void CheckBurnablePossibilities()
    {
        RaycastHit hit;
        Vector3 endPoint = transform.TransformDirection(Vector3.forward);
        bool rayHitSomething = CastBurnableCheckRay(endPoint, out hit);
        if (rayHitSomething)
        {
            //Vector3 point = hit.point;
            targetBurnable = GetBurnableObject(hit);
        }
        else
        {
            if (previouslyHitBurnable != null)
            {
                previouslyHitBurnable.SetMaterial(fireConfig.BurnableDefaultMaterial);
            }
            previouslyHitBurnable = null;
            /*Vector3 point = transform.position + transform.forward * fireConfig.MaxDistance;
            point.y = 0f;*/
        }
        DebugRayCast(hit, rayHitSomething);
    }

    bool CastBurnableCheckRay(Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(
            transform.position,
            endPoint,
            out hit,
            fireConfig.MaxDistance,
            gameConfig.BurnableLayer
        );
    }

    void DebugRayCast(RaycastHit hit, bool rayHitSomething)
    {
        if (gameConfig.VisualDebug)
        {
            Debug.DrawRay(
                transform.position,
                transform.TransformDirection(Vector3.forward) * (rayHitSomething ? hit.distance : playerConfig.MaxDistance),
                rayHitSomething ? playerConfig.DebugRayColorOnHit : playerConfig.DebugRayColor
            );
        }
    }

    Burnable GetBurnableObject(RaycastHit hit)
    {
        BurnableCollider burnableItem = hit.collider.GetComponent<BurnableCollider>();
        if (previouslyHitBurnable != null && previouslyHitBurnable != burnableItem)
        {
            previouslyHitBurnable.SetMaterial(fireConfig.BurnableDefaultMaterial);
        }
        if (burnableItem != null)
        {
            if ((!burnableItem.Burnable.IsLit && playerHandConfig.hasFire))
            {
                previouslyHitBurnable = burnableItem;
                burnableItem.SetMaterial(fireConfig.BurnableHighlightMaterial);
                return burnableItem.Burnable;
            }

        }
        return null;
    }


}
