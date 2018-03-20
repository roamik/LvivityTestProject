﻿import { User } from './user';
import { UserProject } from './UserProject';

export class Project {

    id: string;
    content: string;
    description: string;
    name: string;
    linkedUsers: UserProject[];
    userId: string;
    user: User;

    constructor(id?: string, content?: string, description?: string, name?: string, linkedUsers?: UserProject[], userId?: string, user?: User) {
        this.id = id;
        this.content = content;
        this.description = description;
        this.name = name;
        this.linkedUsers = linkedUsers;
        this.userId = userId;
        this.user = user;
    }

}
