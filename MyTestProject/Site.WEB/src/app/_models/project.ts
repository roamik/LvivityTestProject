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

    image: string;

    constructor(id?: string, content?: string, description?: string, name?: string, linkedUsers?: UserProject[], userId?: string, user?: User, image?: string) {
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
