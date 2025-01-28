using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_EmployeeImage", menuName = "Funcionario/Imagem")]

public class SO_EmployeeImage : ScriptableObject
{
    [SerializeField]
    private List<Sprite> employeeImages;

    public Sprite GetRandomImage()
    {

        System.Random randomGenerator = new System.Random();

        int randomIndex = randomGenerator.Next(0, employeeImages.Count);

        return employeeImages[randomIndex];
    }
}
