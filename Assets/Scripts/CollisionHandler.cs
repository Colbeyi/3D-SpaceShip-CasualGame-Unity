using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    
    AudioSource aS;
    
    
    
    bool isTransitioning = false;
    bool collisionDisabled = false;
    private void Start()
    {
        aS = GetComponent<AudioSource>();
        
    }
    
    void Update()
    {
        Cheats();

    }
    void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                NextLevelDelay();
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }
    
    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        Invoke("ReloadLevel",levelLoadDelay);
        //GetComponent<AudioSource>().enabled = false;
        aS.PlayOneShot(crash);

        
        

    }
    void NextLevelDelay()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        successParticles.Play();
        //GetComponent<AudioSource>().enabled = false;
        Invoke("NextLevel",levelLoadDelay);
        aS.PlayOneShot(success);
        

        

    }
    void ReloadLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex);

    }
    void NextLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentBuildIndex + 1;
        if(nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
        
        
    }
}
