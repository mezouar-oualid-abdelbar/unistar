using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Enum for selecting character
    public enum CharacterType
    {
        Nail,
        Frost,
        Zoro
    }

    [Header("Selection")]
    public CharacterType selectedCharacter;

    [Header("Prefabs")]
    public GameObject nailPrefab;
    public GameObject frostyPrefab;
    public GameObject zoroPrefab;   

    private GameObject currentCharacter;

    void Start()
    {
        SpawnSelectedCharacter();
    }

    void SpawnSelectedCharacter()
    {
        GameObject characterToSpawn = null;

        switch (selectedCharacter)
        {
            case CharacterType.Nail:
                characterToSpawn = nailPrefab;
                break;

            case CharacterType.Frost:
                characterToSpawn = frostyPrefab;
                break;
            case CharacterType.Zoro:
                characterToSpawn = zoroPrefab;
                break;
        }

        if (characterToSpawn == null)
        {
            Debug.LogError("Character prefab is NOT assigned!");
            return;
        }

        // Spawn the selected character
        currentCharacter = Instantiate(
            characterToSpawn,
            transform.position,
            transform.rotation
        );

        // Optional: make it child of this object (keeps hierarchy clean)
        currentCharacter.transform.SetParent(transform);
    }
}