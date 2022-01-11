using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Animator))]
public class ThirdPersonShooterController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera aimCinemachineCamera;
    [SerializeField] float normalSensiitivity = 1f;
    [SerializeField] float aimSensiitivity = 0.7f;

    [Header("Target")]
    [SerializeField] LayerMask aimColliderLayer;
    [SerializeField] Transform targetTr;
    [SerializeField] float targetMoveStep = 0.1f;
    [SerializeField] float rotationStep = 0.1f;

    [Header("Shooting")]
    [SerializeField] float fireRate = 0.1f;
    [SerializeField] Transform spawnBulletTr;

    [Header("Animation")]
    [SerializeField] float animLayerChangeWaigthSpeed = 0.1f;
    [SerializeField] Rig aimRig;

    [Header("FX")]
    [SerializeField] ParticleSystem muzzleFlashe;

    float fireTime;
    float animWeigth;
    Vector2 screenCenterPoint;
    Vector3 mouseWorldPos = Vector3.zero;

    Animator animator;
    ThirdPersonController thirdPersonController;
    StarterAssetsInputs assetsInputs;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        assetsInputs = GetComponent<StarterAssetsInputs>();

        screenCenterPoint = new Vector2(Screen.width/2f, Screen.height/2f);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 500f, aimColliderLayer))
        {
            mouseWorldPos = raycastHit.point;
            targetTr.position = Vector3.Lerp(targetTr.position, raycastHit.point, targetMoveStep);
        }

        aimCinemachineCamera.gameObject.SetActive(assetsInputs.aim);
        thirdPersonController.SetRotateOnMove(!assetsInputs.aim);

        if (assetsInputs.aim)
        {
            thirdPersonController.SetSensiitivity(aimSensiitivity);

            Vector3 worldTarget = mouseWorldPos;
            worldTarget.y = transform.position.y;
            Vector3 aimDirection = (worldTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, rotationStep);

            animWeigth = Mathf.Lerp(animWeigth, 1f, animLayerChangeWaigthSpeed);

            if (fireTime < Time.time && assetsInputs.shoot)
            {
                Shoot();
            }
        }
        else
        {
            thirdPersonController.SetSensiitivity(normalSensiitivity);

            animWeigth = Mathf.Lerp(animWeigth, 0f, animLayerChangeWaigthSpeed);
        }

        animWeigth = Mathf.Clamp01(animWeigth);
        animator.SetLayerWeight(1, animWeigth);
        aimRig.weight = animWeigth;
    }

    void Shoot()
    {
        fireTime = Time.time + fireRate;
        Vector3 aimDir = (targetTr.position - spawnBulletTr.position).normalized;
        ObjectPooler.Instance.GetFromPool(PoolType.PlayerBullet, spawnBulletTr.position, Quaternion.LookRotation(aimDir, Vector3.up));
        CameraController.Instance.ShakeAimCamera();
        muzzleFlashe.Play();
    }
}
