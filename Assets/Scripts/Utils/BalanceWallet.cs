using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceWallet 
{
   
    private float capital;
    private float invoicing;
    private float services;
    private float employees;
    private float others;
    private float endBalance;

    public float Capital { get { return capital; } set { capital = value; } }

    public float Invoicing { get { return invoicing; } set { invoicing = value; } }
    public float Employees { get { return employees; } set { employees = value; } }

    public float Services { get { return services; } set { services = value; } }


    public float Others { get { return others; } set { others = value; } }

    public float EndBalance { get { return endBalance; } set { endBalance = value; } }


    // Start is called before the first frame update

}
