export class ContractResult {

    sender: string;
    receiver: string;
    password: string;
    amount: any;

    constructor(sender?: string, receiver?: string, password?: string, amount?: any) {
        this.sender = sender;
        this.receiver = receiver;
        this.password = password;
        this.amount = amount;
    }
}