using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

    public float metalSphereVelocity;

    private Rigidbody rb;
   
    public GameObject metalSphere;
    public GameObject metalSphere1;
    public GameObject rightElastic;
    public GameObject leftElastic;
    public GameObject rightLine;
    public GameObject leftLine;
    public GameObject leather;
    public GameObject bulletpoint;
    public GameObject leatherLine;
    public GameObject targetpoint;
    private LineRenderer rightElasticLine;
    private LineRenderer leftElasticLine;
    private float pulled;
    private int i;
    private float z;
    public GameObject ENV;
    bool isnotreleased = false;
    void Start ()
    {      
        i = 1;
        //Starts with z = -2 so that the elastic line starts at the size of the elastic.
        z = -2;
        //If the ElasticManager script is not inside the slingshot, the value of pulled is set to - 7.
        if (this.GetComponent<ElasticManager>() != null)
        {
            //Getting elastic resistance value in the ElasticManager script.
            pulled = this.GetComponent<ElasticManager>().elasticResistance - 8;
        }
        else
        {
            pulled = -7;
        }
    }

    
    public void SlingShotActive()
    {
        isnotreleased = false;
        //i receives the value of one so that a new metallic sphere is created when the mouse is pressed again.
        i = 1;
        metalSphere.AddComponent<Rigidbody>();
        rb = metalSphere.GetComponent<Rigidbody>();
        metalSphere.transform.LookAt(targetpoint.transform);
        //Activates the elastic mesh renderer.
        rightElastic.GetComponent<SkinnedMeshRenderer>().enabled = true;
        leftElastic.GetComponent<SkinnedMeshRenderer>().enabled = true;
        leather.GetComponent<SkinnedMeshRenderer>().enabled = true;

        //Disables the elastic line.
        //rightLine.SetActive(false);
        //leftLine.SetActive(false);
        //leatherLine.GetComponent<SkinnedMeshRenderer>().enabled = false;

        //Activates the sphere's gravity as soon as it is thrown.
        rb.useGravity = true;
        metalSphere.transform.SetParent(ENV.transform);
        //Adds a forward force on the rigidbody of the metallic sphere, depending on the amount that the elastic is stretched.
        if (z >= -7)
        {
            //For the elastic to stretch the value of the z axis is increased to - 7, maximum of the stretch.
            //The force that the metallic sphere will be thrown, will be multiplied by the amount of stretch of the elastic.
            rb.AddForce(transform.forward * metalSphereVelocity * -z, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.forward * metalSphereVelocity * 15, ForceMode.Impulse);
        }
        //The metal sphere is taken (parent = null) in the slingshot, so that the sphere stops moving with the slingshot and the camera.
        metalSphere.transform.parent = null;
        z = -2;
    }

    public void SlingShotLoad()
    {
        isnotreleased = true;
        z -= 0.1f;
        if (i == 1)
        {
            //if the i(increment) is equal to one, it means that there is no metal sphere in the slingshot, then the sphere is created to be thrown next.
            metalSphere = Instantiate(metalSphere1, bulletpoint.transform.position, bulletpoint.transform.rotation, bulletpoint.transform);
            metalSphere.transform.LookAt(targetpoint.transform);
            //The metal sphere is parented to the slingshot, so that it can move with the slingshot.
            metalSphere.transform.parent = this.transform;
            i = 0;
        }

        //Disables the elastic mesh renderer.
        //rightElastic.GetComponent<SkinnedMeshRenderer>().enabled = false;
        //leftElastic.GetComponent<SkinnedMeshRenderer>().enabled = false;
        //leather.GetComponent<SkinnedMeshRenderer>().enabled = false;

        //Activates the elastic line, so the movement becomes more fluid and beautiful.
        //rightLine.SetActive(true);
        //leftLine.SetActive(true);
        //leatherLine.GetComponent<SkinnedMeshRenderer>().enabled = true;

        rightElasticLine = rightLine.transform.GetComponent<LineRenderer>();
        leftElasticLine = leftLine.transform.GetComponent<LineRenderer>();

        if (z >= pulled)
        {
            //For the elastic to stretch the value of the z axis is increased to - 7 or pulled value, maximum of the stretch.
            rightElasticLine.SetPosition(1, new Vector3(0, 0, z));
            //The lines are growing and the value of the z axis is increased.
            leftElasticLine.SetPosition(1, new Vector3(0, 0, z));
            //Leather and metallic sphere follow the movement of the line.
          //  metalSphere.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.7f);
            leather.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.2f);
            leatherLine.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.2f);
        }
        else
        {
            //If the z axis value reaches the maximum -7 or pulled value, that value will remain and the slingshot elastic will be completely stretched.
            rightElasticLine.SetPosition(1, new Vector3(0, 0, pulled));
            leftElasticLine.SetPosition(1, new Vector3(0, 0, pulled));
          //  metalSphere.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.7f);
            leather.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.2f);
            leatherLine.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.2f);
        }
    }

    private void Update()
    {
        if (isnotreleased == true)
        {
            metalSphere.transform.position = bulletpoint.transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            z -= 0.1f;
            if (i == 1)
            {
                //if the i(increment) is equal to one, it means that there is no metal sphere in the slingshot, then the sphere is created to be thrown next.
                metalSphere = Instantiate(metalSphere1, leather.transform.position, leather.transform.rotation, leather.transform);

                //The metal sphere is parented to the slingshot, so that it can move with the slingshot.
                //metalSphere.transform.parent = this.transform;
                i = 0;
            }

            //Disables the elastic mesh renderer.
            //rightElastic.GetComponent<SkinnedMeshRenderer>().enabled = false;
            //leftElastic.GetComponent<SkinnedMeshRenderer>().enabled = false;
            //leather.GetComponent<SkinnedMeshRenderer>().enabled = false;

            //Activates the elastic line, so the movement becomes more fluid and beautiful.
            //rightLine.SetActive(true);
            //leftLine.SetActive(true);
            //leatherLine.GetComponent<SkinnedMeshRenderer>().enabled = true;

            rightElasticLine = rightLine.transform.GetComponent<LineRenderer>();
            leftElasticLine = leftLine.transform.GetComponent<LineRenderer>();

            if (z >= pulled)
            {
                //For the elastic to stretch the value of the z axis is increased to - 7 or pulled value, maximum of the stretch.
                rightElasticLine.SetPosition(1, new Vector3(0, 0, z));
                //The lines are growing and the value of the z axis is increased.
                leftElasticLine.SetPosition(1, new Vector3(0, 0, z));
                //Leather and metallic sphere follow the movement of the line.
                //  metalSphere.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.7f);
                leather.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.2f);
                leatherLine.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.2f);
            }
            else
            {
                //If the z axis value reaches the maximum -7 or pulled value, that value will remain and the slingshot elastic will be completely stretched.
                rightElasticLine.SetPosition(1, new Vector3(0, 0, pulled));
                leftElasticLine.SetPosition(1, new Vector3(0, 0, pulled));
                //  metalSphere.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.7f);
                leather.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.2f);
                leatherLine.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.2f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //i receives the value of one so that a new metallic sphere is created when the mouse is pressed again.
            i = 1;
            metalSphere.AddComponent<Rigidbody>();
            rb = metalSphere.GetComponent<Rigidbody>();
            //Activates the elastic mesh renderer.
            rightElastic.GetComponent<SkinnedMeshRenderer>().enabled = true;
            leftElastic.GetComponent<SkinnedMeshRenderer>().enabled = true;
            leather.GetComponent<SkinnedMeshRenderer>().enabled = true;

            //Disables the elastic line.
            //rightLine.SetActive(false);
            //leftLine.SetActive(false);
            //leatherLine.GetComponent<SkinnedMeshRenderer>().enabled = false;

            //Activates the sphere's gravity as soon as it is thrown.
            rb.useGravity = true;
            metalSphere.transform.SetParent(ENV.transform);
            //Adds a forward force on the rigidbody of the metallic sphere, depending on the amount that the elastic is stretched.
            if (z >= -7)
            {
                //For the elastic to stretch the value of the z axis is increased to - 7, maximum of the stretch.
                //The force that the metallic sphere will be thrown, will be multiplied by the amount of stretch of the elastic.
                rb.AddForce(transform.forward * metalSphereVelocity * -z, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(transform.forward * metalSphereVelocity * 15, ForceMode.Impulse);
            }
            //The metal sphere is taken (parent = null) in the slingshot, so that the sphere stops moving with the slingshot and the camera.
            metalSphere.transform.parent = null;
            z = -2;
        }
    }
}
