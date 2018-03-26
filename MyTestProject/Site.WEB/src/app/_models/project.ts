import { User } from './user';
import { UserProject } from './UserProject';

export class Project {

    id: string;
    content: string;
    description: string;
    name: string;
    linkedUsers: UserProject[];
    userId: string;
    user: User;

    image: any;

    constructor(id?: string, content?: string, description?: string, name?: string, linkedUsers?: UserProject[], userId?: string, user?: User, image?: any) {
        this.id = id;
        this.content = content;
        this.description = description;
        this.name = name;
        this.linkedUsers = linkedUsers;
        this.userId = userId;
        this.user = user;
        this.image = image;
    }

}
