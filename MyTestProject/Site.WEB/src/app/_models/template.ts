export class Template {
    id: number;
    content: string;
    description: string;
    name: string;
    userId: string;

    constructor(id?: number, content?: string, description?: string, name?: string, userId?: string) {
        this.id = id;
        this.content = content;
        this.description = description;
        this.name = name;
        this.userId = userId;
    }
}