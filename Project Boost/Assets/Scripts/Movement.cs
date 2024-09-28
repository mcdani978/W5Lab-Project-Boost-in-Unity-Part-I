using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float thrustForce = 10f;
    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;

    public float fuel = 100f; // Variable for fuel level
    public float fuelConsumptionRate = 10f; // Fuel consumption rate per second
    public TextMeshProUGUI fuelText; // Reference to the TextMeshProUGUI for displaying fuel

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateFuelText(); // Initial fuel display
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ProcessHorizontalMovement();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0) // Check if space is held down and there is fuel
        {
            rb.AddRelativeForce(Vector3.up * thrustForce);
            ConsumeFuel(); // Only consume fuel while holding space
        }
    }

    void ProcessRotation()
    {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
    }

    void ProcessHorizontalMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void ConsumeFuel() // Method to reduce fuel
    {
        fuel -= fuelConsumptionRate * Time.deltaTime; // Decrease fuel while space is held down
        fuel = Mathf.Clamp(fuel, 0, 100); // Ensure fuel stays between 0 and 100
        UpdateFuelText(); // Update fuel display
    }

    void UpdateFuelText() // Method to update the fuel text on UI
    {
        fuelText.text = "Fuel: " + Mathf.RoundToInt(fuel).ToString(); // Display the current fuel value
    }
}
