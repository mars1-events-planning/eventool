export type Todo = {
    title: string;
    done: boolean;
}

export type Checklist = {
    id: string;
    title: string;
    items: Todo[];
}

export type EventModel = {
    id: string;
    title: string;
    description: string | null;
    address: string | null;
    startAtUtc: string | null;
    checklists: Checklist[];
}