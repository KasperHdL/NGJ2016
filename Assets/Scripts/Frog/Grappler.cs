using UnityEngine;
using System.Collections;

public class Grappler : MonoBehaviour {

    [HideInInspector]
    public SpringJoint2D joint;
    [HeaderAttribute("Physics")]
    
    public float pullLength;

    private float toungeLength;
    
    public Tounge tounge;
    public GameObject tounge_prefab;
    
    public bool isToungeOut = false;
    
    public bool isToungeJointed;
    
    public float angularSpeed;
    public bool isToungePulling;
    
    private Rigidbody2D body;
    private LineRenderer lineRenderer;
    
    public Sprite spriteToungeIn;
    public Sprite spriteToungeOut;
    
    private SpriteRenderer spriteRenderer;
    
    public AudioSource frogSoundSource;
    public AudioClip[] SOUND_TongueConnect;
    public AudioClip[] SOUND_TongueIn;
    public AudioClip[] SOUND_TongueOut;
    public AudioClip[] SOUND_Splash;

    public float waterLevel;

    private bool hasPlayedSplashSound;

    private int ranX;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteToungeIn;
        body = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        if(tounge == null)
            tounge = ((GameObject)Instantiate(tounge_prefab,Vector3.zero,Quaternion.identity)).GetComponent<Tounge>();
        
        tounge.grappler = this;
        
        joint = GetComponent<SpringJoint2D>();
        joint.connectedBody = tounge.GetComponent<Rigidbody2D>();
        joint.enabled = false;
        
    }
   
    
    void Update(){
        if(isToungeOut){
            Vector2 v = (tounge.transform.position - transform.position);
            toungeLength = v.magnitude;
            transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * (Mathf.Atan2(v.y,v.x)));
            lineRenderer.SetPosition(0,transform.position);
            lineRenderer.SetPosition(1,tounge.transform.position);
        }

        if(transform.position.y <= waterLevel && !hasPlayedSplashSound){
            ranX = Random.Range(0, SOUND_Splash.Length);
            frogSoundSource.clip = SOUND_Splash[ranX];
            frogSoundSource.Play();

            hasPlayedSplashSound = true;
        }
        if(hasPlayedSplashSound && transform.position.y > waterLevel)
            hasPlayedSplashSound = false;
    }

	
    public void ShootTounge(Vector2 dir){
        isToungeOut = true;
        tounge.transform.position = transform.position;
        tounge.gameObject.SetActive(true);
        joint.enabled = false;
        lineRenderer.enabled = true;
        tounge.ShootTounge(dir);
        spriteRenderer.sprite = spriteToungeOut;

        ranX = Random.Range(0, SOUND_TongueOut.Length);

        frogSoundSource.clip = SOUND_TongueOut[ranX];
        frogSoundSource.Play();
    }
    
    public void RetractTounge(){
        isToungeOut = false;
        joint.enabled = false;
        lineRenderer.enabled = false;
        spriteRenderer.sprite = spriteToungeIn;
        
        
        if(isToungeJointed){
            Vector2 d = (transform.position - tounge.transform.position).normalized;
            float angle = Vector2. Angle(d,body.velocity);
            //Debug.Log("d: " + d + ", b: " + body.velocity.normalized + " angle: " + angle);
            body.angularVelocity = angularSpeed * Mathf.Sign(-angle) * body.velocity.magnitude;
        }
        
        isToungeJointed = false;
        tounge.gameObject.SetActive(false);

        ranX = Random.Range(0, SOUND_TongueIn.Length);

        frogSoundSource.clip = SOUND_TongueIn[ranX];
        frogSoundSource.Play();
    }
    
    public void ToungeHit(){
        joint.enabled = true;
        toungeLength = (tounge.transform.position - transform.position).magnitude;
        joint.distance = toungeLength;
        
        
        if(toungeLength > tounge.transform.position.y || transform.position.y < 0){ //change when water is in the game
            joint.frequency = 1.5f * (toungeLength - pullLength) / pullLength;
            joint.distance = tounge.transform.position.y;
            
        }
                
        isToungeJointed = true;

        ranX = Random.Range(0, SOUND_TongueConnect.Length);

        frogSoundSource.clip = SOUND_TongueConnect[ranX];
        frogSoundSource.Play();
    }
    
}
