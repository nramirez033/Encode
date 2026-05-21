import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Task, CreateTaskRequest, UpdateTaskStatusRequest } from '../../models/task.models';
import { environment } from '../../../enviroment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private http = inject(HttpClient);
  private api = environment.apiUrl;

  getTasks() {
    return this.http.get<Task[]>(
      `${this.api}/tasks`
    );
  }

  createTask(data: CreateTaskRequest) {
    return this.http.post(
      `${this.api}/tasks`,
      data
    );
  }

  updateStatus(id: string, data: UpdateTaskStatusRequest) {
    return this.http.put(
      `${this.api}/tasks/${id}/status`,
      data
    );
  }

  deleteTask(id: string) {
    return this.http.delete(
      `${this.api}/tasks/${id}`
    );
  }
}
