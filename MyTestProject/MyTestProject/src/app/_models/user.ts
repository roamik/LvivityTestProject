export class User {
  id: string;
  userName: string;
  firstName: string;
  

  constructor(id?: string, userName?: string, firstName?: string) {
    this.id = id;
    this.userName = userName;
    this.firstName = firstName;
  }
}
