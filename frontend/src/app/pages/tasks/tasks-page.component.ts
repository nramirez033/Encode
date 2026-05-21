import {Component,inject,OnInit} from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import {Task, TaskStatus} from '../../models/task.models';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../core/services/task.service';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';


@Component({
  selector: 'app-tasks-page',
  standalone: true,
  imports: [ CommonModule, ReactiveFormsModule ],
  templateUrl: './tasks-page.component.html'
})
export class TasksPageComponent
implements OnInit {

  private service = inject(TaskService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private auth = inject(AuthService);

  TaskStatus = TaskStatus;
  user: any;

  tasks: Task[] = [];
  modal: any;
  taskToDeleteId?: string;

  form = this.fb.nonNullable.group({
    titulo: [''],
    descripcion: ['']
  });

  ngOnInit(): void {
    this.auth
      .getMe()
      .subscribe(
        user =>
          this.user = user
      );

    this.load();
  }

  load() {
    this.service
      .getTasks()
      .subscribe(
        tasks =>
          this.tasks = tasks
      );
  }

  create(): void {
    if (this.form.invalid) {
      return;
    }

    this.service.createTask(this.form.getRawValue())
      .subscribe({
        next: () => {
          this.form.reset({
            titulo: '',
            descripcion: ''
          });

          this.load();

          const modalElement = document.getElementById('createTaskModal');

          if (modalElement) {
            const modal =(window as any).bootstrap?.Modal.getInstance(modalElement);
            modal?.hide();
          }
        }
      });
  }

  nextStatus(task: Task) {
    if (task.estado === TaskStatus.Pending) {
      this.changeStatus(task, TaskStatus.InProgress);
    }
    else if (task.estado === TaskStatus.InProgress) {
      this.changeStatus(task, TaskStatus.InProgress);
    }
  }

  changeStatus(task: Task, estado: TaskStatus) {
    this.service.updateStatus(task.id, { estado })
      .subscribe(() =>
        this.load()
      );
  }

  getStatusLabel(status: TaskStatus): string {
    switch (status) {
      case TaskStatus.Pending:
        return 'Pendiente';
      case TaskStatus.InProgress:
        return 'En progreso';
      case TaskStatus.Done:
        return 'Finalizada';
      default:
        return '';
    }
  }


  openDeleteModal(id: string): void {
    this.taskToDeleteId = id;

    const modalElement = document.getElementById('deleteTaskModal');

    if (!modalElement) {
      return;
    }

    const modal = (window as any).bootstrap.Modal.getOrCreateInstance(modalElement);
    modal.show();
  }

  confirmDelete(): void {
    if (!this.taskToDeleteId) {
      return;
    }

    this.service.deleteTask(this.taskToDeleteId)
      .subscribe({
        next: () => {
          this.load();

          const modalElement = document.getElementById('deleteTaskModal');

          const modal = (window as any).bootstrap.Modal.getInstance(modalElement);

          modal?.hide();
          this.taskToDeleteId = undefined;
        }
      });
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/auth']);
  }
}
