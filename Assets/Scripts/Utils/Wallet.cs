using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet
{
    private float capital;
    private float balance;
    private float mensalCosts;
    private float invoicing;

    public float Capital{ get { return capital; } set { capital = value; }}

    public float Balance{ get { return balance; } set { balance = value; }}

    public float MensalCosts{ get { return mensalCosts; } set { mensalCosts = value; }}

    public float Invoicing{ get { return invoicing; } set { invoicing = value; }}
}

