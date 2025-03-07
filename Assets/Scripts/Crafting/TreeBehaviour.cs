using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int maxAmount;
    [SerializeField] private ParticleSystem leafsParticle;

    // Seria possível pegar o Animator com GetComponent<Animator>.
    // Apenas para mostrar que também é possível passar como parâmetro no editor.
    [SerializeField] private Animator animator;

    private bool isCut;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCut) { return; }
        
        if (other.CompareTag("Axe"))
        {
            OnHit();
        }
    }
    
    public void OnHit()
    {
        treeHealth--;
        animator.SetTrigger("isHit");
        leafsParticle.Play();

        if (treeHealth <= 0)
        {
            for (int i = 0; i < Random.Range(1, maxAmount); i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), transform.rotation);    
            }
            
            animator.SetTrigger("cut");
            isCut = true;
        }
    }
}