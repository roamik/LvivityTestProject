import { User } from './user';

export class Project {

    id: string;
    content: string;
    description: string;
    name: string;
    linkedUsers: any[];
    user:User;


    constructor(id?: string, content?: string, description?: string, name?: string, linkedUsers?: any[], user?:User) {
        this.id = id;
        this.content = content;
        this.description = description;
        this.name = name;
        this.linkedUsers = linkedUsers;
        this.user = user;
    }

}
