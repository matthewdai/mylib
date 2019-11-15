
export interface Project {
    id: number;
    group?: string;
    team?: string;
    name: string;
    owner?: string;
    description?: string;
    dateCreated?: Date;
    datModified?: Date;
    detail: boolean;
}