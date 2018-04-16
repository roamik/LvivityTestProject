export class Transaction {

    sender: string;
    receiver: string;
    password: string;
    amount: number;
    confirmed: boolean;

    constructor(sender?: string, receiver?: string, password?: string, amount?: number, confirmed?: boolean) {
        this.sender = sender;
        this.receiver = receiver;
        this.password = password;
        this.amount = amount;
        this.confirmed = confirmed;
    }
}