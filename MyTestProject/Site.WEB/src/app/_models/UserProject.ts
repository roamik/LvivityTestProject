import { User } from './user';
import { Project } from './project';

export class UserProject {
    
    userId: string;

    user: User;

    projectId: string;

    project: Project;


    constructor(userId?: string, user?: User, projectId?: string, project?: Project) {
        this.userId = userId;
        this.user = user;
        this.projectId = projectId;
        this.project = project;
    }

}
