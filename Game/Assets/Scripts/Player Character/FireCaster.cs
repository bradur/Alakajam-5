// Date   : 23.02.2019 14:06
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

public class FireCaster : MonoBehaviour
{

    private PlayerConfig playerConfig;
    private GameConfig gameConfig;
    private FireConfig fireConfig;
    private PlayerHandConfig playerHandConfig;
    private FireSourceManager fireSource;

    private TorchCollider previouslyHitTorch;
    private FireSource targetFireSource;

    void Start()
    {
        playerConfig = ConfigManager.main.GetConfig("PlayerConfig") as PlayerConfig;
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        fireConfig = ConfigManager.main.GetConfig("FireConfig") as FireConfig;
        playerHandConfig = ConfigManager.main.GetConfig("PlayerHandConfig") as PlayerHandConfig;
    }

    void Update()
    {
        CheckFirePossibilities();
        ProcessInput();
    }

    void ProcessInput()
    {
        if (KeyManager.main.GetKeyDown(PlayerAction.ExtinguishOrLightFire))
        {
            ExtinguishOrLightFire();
        }
    }

    void ExtinguishOrLightFire()
    {
        previouslyHitTorch = null;
        CheckFirePossibilities();
        if (targetFireSource != null)
        {
            if (targetFireSource.IsLit)
            {
                targetFireSource.Extinguish();
                AudioManager.main.PlaySound(SoundType.Extinguish);
                playerHandConfig.triggerGrab = true;
            }
            else
            {
                targetFireSource.Light();
                AudioManager.main.PlaySound(SoundType.LightFire);
                playerHandConfig.triggerThrow = true;
            }
        }
    }

    void CheckFirePossibilities()
    {
        RaycastHit hit;
        RaycastHit burnHit;
        Vector3 endPoint = transform.TransformDirection(Vector3.forward);
        bool rayHitSomething = CastTorchCheckRay(endPoint, out hit);
        bool rayHitBurnTarget = CastBurnCheckRay(endPoint, out burnHit);
        if (rayHitSomething && !rayHitBurnTarget)
        {
            //Vector3 point = hit.point;
            targetFireSource = GetInteractableFireSource(hit);
        }
        else if(rayHitSomething && rayHitBurnTarget && burnHit.distance > hit.distance)
        {
            targetFireSource = GetInteractableFireSource(hit);
        }
        else
        {
            if (previouslyHitTorch != null)
            {
                previouslyHitTorch.SetMaterial(fireConfig.TorchDefaultMaterial);
            }
            targetFireSource = null;
            /*Vector3 point = transform.position + transform.forward * fireConfig.MaxDistance;
            point.y = 0f;*/
        }
        DebugRayCast(hit, rayHitSomething);
    }

    bool CastTorchCheckRay(Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(
            transform.position,
            endPoint,
            out hit,
            fireConfig.MaxDistance,
            gameConfig.TorchLayer | gameConfig.WallLayer | gameConfig.BurnableLayer
        );
    }

    bool CastBurnCheckRay(Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(
            transform.position,
            endPoint,
            out hit,
            fireConfig.MaxDistance,
            gameConfig.BurnableLayer | gameConfig.WallLayer | gameConfig.BurnableLayer
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

    FireSource GetInteractableFireSource(RaycastHit hit)
    {
        TorchCollider torch = hit.collider.GetComponent<TorchCollider>();
        if (previouslyHitTorch != null && previouslyHitTorch != torch)
        {
            previouslyHitTorch.SetMaterial(fireConfig.TorchDefaultMaterial);
        }
        if (torch != null)
        {
            if ((!torch.FireSource.IsLit && playerHandConfig.hasFire) || (torch.FireSource.IsLit && !playerHandConfig.hasFire))
            {
                if (hit.distance < fireConfig.MinDistance || !torch.FireSource.Interactable)
                {
                    previouslyHitTorch = torch;
                    torch.SetMaterial(fireConfig.TorchDeniedMaterial);
                    return null;
                } else
                {
                    previouslyHitTorch = torch;
                    torch.SetMaterial(fireConfig.TorchHighlightMaterial);
                    return torch.FireSource;
                }
            }

        }
        return null;
    }


}
