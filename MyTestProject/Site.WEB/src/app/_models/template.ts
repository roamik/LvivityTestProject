export class Template {
    
    id: string;
    content: string;
    description: string;
    name: string;

    constructor(id?: string, content?: string, description?: string, name?: string) {
        this.id = id;
        this.content = content;
        this.description = description;
        this.name = name;
    }
}