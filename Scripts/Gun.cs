using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    //Из другого видео
    public GameObject Ak47;
    public int ammoCount;
    public int currentAmmo;
    public int ammo;
    public VideoClip Fire;
    public float shootSpeed;
    public float reloadSpeed;
    public VideoClip Reload;
    public float reloadTimer = 0f;
    public float shootTimer = 0f;
    public bool reloadAnim = false;

    void Update()
    {
        if (Input.GetButton("Fire1") & currentAmmo >= 0 & reloadTimer <= 0 & shootTimer <= 0)
        {
            if (reloadAnim == false)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadAnim = !reloadAnim;
            Ak47.GetComponent<Animator>().SetTrigger("ReloadTrigger");
            currentAmmo = ammo;
            reloadTimer = reloadSpeed;
            //GetComponent<AudioSource>().PlayOneShot("Reload");
            Invoke("DontShoot", 1.5f);
        }

        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
        }
        if (reloadTimer > 0f)
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    public void DontShoot()
    {
        reloadAnim = !reloadAnim;
    }

    public void Shoot()
    {
        muzzleFlash.Play();
        Ak47.GetComponent<Animator>().SetTrigger("ShootingTrigger");

        RaycastHit hit;
        currentAmmo = currentAmmo - 1;
        //GetComponent<AudioSource>().PlayOneShot("Fire");
        shootTimer = shootSpeed;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
