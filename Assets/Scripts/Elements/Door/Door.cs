using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Door : Effector
{
    [SerializeField] private float _attractionForce;
    [SerializeField] private float _minDistance;
    [SerializeField] private PanelBonesToNextLevel _panel;
    [SerializeField] private AudioClip _openDoor;

    private int _bonesToNextLevel;

    public UnityAction LevelPassed; 

    public void Initialize(int bonesToNextLevel)
    {
        _bonesToNextLevel = bonesToNextLevel;
        _panel.Renderer(_bonesToNextLevel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Snake>(out Snake snake))
        {
            if (snake.Tails.Count >= _bonesToNextLevel)
            {
                StartCoroutine(Tighten(snake.Tails));
                snake.WalkedThroughDoor?.Invoke();
                LevelPassed?.Invoke();
                AudioSource.PlayOneShot(_openDoor);
            }
            else
            {
                snake.Die();
            }
        }
    }

    private IEnumerator Tighten(List<Bone> bones)
    {
        bones.ForEach(bone => bone.Rigidbody.isKinematic=false);
        bones.ForEach(bone => bone.Collider.enabled=false);

        while (bones.Any(bone=>bone.gameObject.activeSelf==true))
        {
            foreach (var bone in bones)
            {
                Vector3 direction = (transform.position - bone.transform.position).normalized;
                float distance = Vector3.Distance(bone.transform.position, transform.position);

                if (distance > _minDistance)
                {
                    float forceMagnitude = _attractionForce / (distance);
                    bone.Rigidbody.AddForce(direction * forceMagnitude, ForceMode.Acceleration);
                }
                else
                {
                    bone.gameObject.SetActive(false);
                    bone.Rigidbody.isKinematic = true;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    protected override void Animate()
    {
        
    }
}
