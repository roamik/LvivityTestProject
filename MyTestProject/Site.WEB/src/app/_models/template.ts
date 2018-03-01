export class Template {
    id: number;
    content: string;
    description: string;
    name: string;

    constructor(id?: number, content?: string, description?: string, name?: string) {
        this.id = id;
        this.content = content;
        this.description = description;
        this.name = name;
    }
}