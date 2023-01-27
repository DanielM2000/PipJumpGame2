using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    //this script is used to check whether our player has got the correct key.
    [SerializeField] DoorBehaviour _doorBehaviour;

    [SerializeField] bool _isDoorOpenSwitch;
    [SerializeField] bool _isDoorClosedSwitch;

    float _switchSizeY;
    Vector3 _switchUpPos;
    Vector3 _switchDownPos;
    float _switchSpeed = 1f;
    float _switchDelay = 0.2f;
    bool _isPressingSwitch = false;

    [SerializeField] InventoryManager.AllItems _requiredItem;



    // Start is called before the first frame update
    void Start()
    {
        _switchSizeY = transform.localScale.y / 2;

        _switchUpPos = transform.position;
        _switchDownPos = new Vector3(transform.position.x,
            transform.position.y - _switchSizeY, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isPressingSwitch)
        {
            MoveSwitchDown();

        }
        else if (_isPressingSwitch)
        {
            MoveSwitchUp();
        }

        void MoveSwitchDown()
        {
            if (transform.position != _switchDownPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _switchDownPos,
                    _switchSpeed * Time.deltaTime);
            }
        }

        void MoveSwitchUp()
        {
            if (transform.position != _switchUpPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _switchUpPos,
                    _switchSpeed * Time.deltaTime);
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPressingSwitch = !_isPressingSwitch;

            if (hasRequiredItem(_requiredItem))
            {
                if (_isDoorOpenSwitch && !_doorBehaviour._isDoorOpen)
                {
                    _doorBehaviour._isDoorOpen = !_doorBehaviour._isDoorOpen;
                }
                else if (_isDoorClosedSwitch && _doorBehaviour._isDoorOpen)
                {
                    _doorBehaviour._isDoorOpen = !_doorBehaviour._isDoorOpen;
                }
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SwitchUpDelay(_switchDelay));
        }
    }

    IEnumerator SwitchUpDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isPressingSwitch = false;
    }

    public bool hasRequiredItem(InventoryManager.AllItems itemRequired)
    {
        if (InventoryManager.instance._inventoryItems.Contains
            (itemRequired))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

