using UnityEngine;
using System.Collections.Generic;
public class G : MonoBehaviour
{
    private Rigidbody rb;
 
    const float Ga = 0.006674f; //Gravitational Constant 6.674
 
    //create a List of objects in the galaxy to attract
    public static List< G > otherObjectsList;
 
    private bool planet = false;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
 
        //create a list for the first time
        if (otherObjectsList == null)
        {
            otherObjectsList = new List< G >();
        }
 
        //add object (with gravity script) to attract to the list
        otherObjectsList.Add(this);
 
    }// Awake
 
    void FixedUpdate()
    {
        for (int i = 0; i < otherObjectsList.Count; i++)
        {
            if (otherObjectsList[i] == null) continue; // ข้ามถ้า object ถูกทำลาย
            Attract(otherObjectsList[i]);
        }
 
    }// FixedUpdate
 
 
    void Attract(G other)
    {
        if (rb == null || other == null || other.rb == null) return; // ป้องกันข้อผิดพลาด

        Rigidbody otherRb = other.rb;

        // ตรวจสอบอีกครั้งว่า Rigidbody ยังไม่ถูกทำลาย
        if (otherRb == null || rb == null) return;

        // Direction
        Vector3 direction = rb.position - otherRb.position;

        // Distance
        float distance = direction.magnitude;

        // ป้องกันข้อผิดพลาดเมื่อวัตถุอยู่ตำแหน่งเดียวกัน
        if (distance == 0) return;

        // คำนวณแรงดึงดูด
        float forceMagnitude = Ga * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

        // Gravitation
        Vector3 gravityForce = forceMagnitude * direction.normalized;

        // Attract
        otherRb.AddForce(gravityForce);
    }

   
}
