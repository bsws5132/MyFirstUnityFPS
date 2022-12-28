using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

	// ���� ������
	public string weaponName;
	public int bulletsPerMag;
	public int bulletsTotal;
	public int currentBullets;
	public float range;
	public float fireRate;
	public float accuracy;
	public float damage;
	public float headshotMultiples;
    public float Aim_LessRecoilMultiples; // �����ؽ� ���ҵǴ� �ݵ� ���
	public float Aim_LessFireRateMultiples; // �����ؽ� ���ҵǴ� ����ӵ� ���
	public float Aim_accuracyMultiples; // �����ؽ� �����ϴ� ��Ȯ�� ���
   
	public Vector3 aimPosition;
	private Vector3 originalPosition;
	
	private float originalAccuracy;
	private float originalFirerate;

	// �Ķ����
	private float fireTimer;
	private bool isReloading = false;
	private bool isAiming;
	private bool isRunning;

	// ���۷���
	public Transform shootPoint;
	public ParticleSystem muzzleFlash;
	public Text bulletsText;
	public Transform bulletCasingPoint;
	private Animator anim;
	private CharacterController characterController;

	//������
	public GameObject hitSparkPrefab;
	public GameObject hitHolePrefab;
	public GameObject bulletCasing;

	// ����
	public AudioClip shootSound;
	public AudioClip reloadSound;
	public AudioSource audioSource;

	// �ݵ�
	public Transform camRecoil;
	public Vector3 recoilKickback;
	public float recoilAmount;
	private float originalRecoil;


	// Use this for initialization
	private void Start()
	{
		currentBullets = bulletsPerMag;
		anim = GetComponent<Animator>();
		bulletsText.text = currentBullets + " / " + bulletsTotal;
		originalPosition = transform.localPosition;
		originalAccuracy = accuracy;
		originalRecoil = recoilAmount;
		originalFirerate = fireRate;
		characterController = GetComponentInParent<CharacterController>();
	}

	// Update is called once per frame
	private void Update()
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
		isReloading = info.IsName("Reload");

		if (Input.GetButton("Fire1"))
		{
			if (currentBullets > 0)
			{
				Fire();
			}
			else
			{
				DoReload();
			}
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			DoReload();
		}

		if (fireTimer < fireRate)
		{
			fireTimer += Time.deltaTime;
		}
		AimDownSights();
		RecoilBack();
		Run();
	
	}

    

    private void Fire()  // ���콺 ���� Ŭ�� ������ �ѿ��� �Ѿ� ������ ���ִ� �Լ�
	{
		if (fireTimer < fireRate || isReloading || isRunning) return;
		Debug.Log("Shot Fired!");
		RaycastHit hit;
		if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward + Random.onUnitSphere * accuracy, out hit, range))
		{
			HealthManager healthManager = hit.transform.GetComponentInParent<HealthManager>();
			BtHealthManager blink = hit.transform.GetComponent<BtHealthManager>();
			BarrelHealth barrel = hit.transform.GetComponent<BarrelHealth>();
			
			if (healthManager)
			{

				if(hit.transform.tag == "TargetHead")
                {
					Debug.Log("HEAD!");
					healthManager.ApplyDamage(damage * headshotMultiples);

				}
				else
                {
					Debug.Log("BODY!");
					healthManager.ApplyDamage(damage);

				}
			}
			else if(blink)
            {
				if(hit.transform.tag == "BlinkTarget")
                {
					Debug.Log("Blink!");
					blink.ApplyDamage(damage);
					GameObject.Find("UI").GetComponent<ScoreManager>().score += 100;
					GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score += 100;

				}
            }
			else if(barrel)
            {
				Debug.Log("Barrel!");
				barrel.ApplyDamage(damage);
				GameObject.Find("UI").GetComponent<ScoreManager>().score -= 110;
				GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score -= 110;

			}				
			
			

			
			GameObject hitSpark = Instantiate(hitSparkPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
			hitSpark.transform.SetParent(hit.transform);
			Destroy(hitSpark, 0.25f);
			GameObject hitHole = Instantiate(hitHolePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
			hitHole.transform.SetParent(hit.transform);
			Destroy(hitHole, 2f);
     
			Debug.Log("Hit!");

		}
		currentBullets--;
		fireTimer = 0.0f;
		audioSource.PlayOneShot(shootSound); // shoot sound
		anim.CrossFadeInFixedTime("Fire", 0.01f); // fire animation
		muzzleFlash.Play();
		bulletsText.text = currentBullets + " / " + bulletsTotal;
		Recoil();
		BulletEffect();
		
	}

	private void AimDownSights() // ������
	{
		
		if (Input.GetButton("Fire2") && !isReloading && !isRunning)  // ���콺 ������ ��ư�� �� ������ ������ + ������, �޸��� ���� �ƴҋ�
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * 12f);
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 40f, Time.deltaTime * 12f);
			
			accuracy = originalAccuracy / Aim_accuracyMultiples;
			recoilAmount = originalRecoil / Aim_LessRecoilMultiples;
			fireRate = originalFirerate * Aim_LessFireRateMultiples;
			isAiming = true;
			
		}
		else
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 5f);
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * 8f);
			accuracy = originalAccuracy;
			recoilAmount = originalRecoil;
			fireRate = originalFirerate;
			isAiming = false;
		}
	}

	private void Recoil() //�ѱ� �ݵ�
    {
		Vector3 recoilVector = new Vector3(Random.Range(-recoilKickback.x, recoilKickback.x), recoilKickback.y, recoilKickback.z);
		Vector3 recoilCamVector = new Vector3(-recoilVector.y * 400f, recoilVector.x * 200f, 0);
		transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + recoilVector, recoilAmount / 2f);
		camRecoil.localRotation = Quaternion.Slerp(camRecoil.localRotation, Quaternion.Euler(camRecoil.localEulerAngles + recoilCamVector), recoilAmount);

    }
	private void RecoilBack() // �ݵ� ȸ��
	{
		camRecoil.localRotation = Quaternion.Slerp(camRecoil.localRotation, Quaternion.identity, Time.deltaTime * 3f);
	}

	private void DoReload() //������ �ִϸ��̼�
	{
		if (!isReloading && currentBullets < bulletsPerMag && bulletsTotal > 0)
		{
			anim.CrossFadeInFixedTime("Reload", 0.01f); // Reload
			audioSource.PlayOneShot(reloadSound);
		}
	}

	private void BulletEffect() // ź�� ����
	{
		Quaternion randomQuaternion = new Quaternion(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f), 1);
		GameObject casing = Instantiate(bulletCasing, bulletCasingPoint);
		casing.transform.localRotation = randomQuaternion;
		casing.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(50f, 100f), Random.Range(50f, 100f), Random.Range(-30f, 30f)));
		Destroy(casing, 1f);
	}


	public void Reload() // ������ �۵�
	{
		int bulletsToReload = bulletsPerMag - currentBullets;
		if (bulletsToReload > bulletsTotal)
		{
			bulletsToReload = bulletsTotal;
		}
		currentBullets += bulletsToReload;
		bulletsTotal -= bulletsToReload;
		bulletsText.text = currentBullets + " / " + bulletsTotal;
	}

	private void Run() // �޸� �� �ִϸ��̼�

	{
		anim.SetBool("isRunning", Input.GetKey(KeyCode.LeftShift));
		isRunning = characterController.velocity.sqrMagnitude > 50 ? true : false;
		anim.SetFloat("Speed", characterController.velocity.sqrMagnitude);
		
	}
	
}
