export enum TaskStatus {
  Pending = 0,
  InProgress = 1,
  Done = 2
}

export interface Task {
  id: string;
  titulo: string;
  descripcion: string;
  estado: TaskStatus;
  creadoPor?: string;
}

export interface CreateTaskRequest {
  titulo: string;
  descripcion: string;
}

export interface UpdateTaskStatusRequest {
  estado: TaskStatus;
}
