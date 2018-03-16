export class Project {

    id: string;
    content: string;
    description: string;
    name: string;
    linkedUsers: any[];

    constructor(id?: string, content?: string, description?: string, name?: string) {
        this.id = id;
        this.content = content;
        this.description = description;
        this.name = name;
    }

}
