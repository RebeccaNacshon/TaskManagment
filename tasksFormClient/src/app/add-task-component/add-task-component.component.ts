import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { TaskService } from '../task.service';
import { Task } from '../task';

@Component({
  selector: 'app-add-task',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatInputModule, MatButtonModule, MatCardModule],
  templateUrl: './add-task-component.component.html',
  styleUrls: ['./add-task-component.component.css']
})
export class AddTaskComponentComponent implements OnInit {
  taskForm!: FormGroup;

  constructor(private fb: FormBuilder, private taskService: TaskService) {}

  ngOnInit(): void {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      dueDate: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.taskForm.valid) {
      const newTask: Task = this.taskForm.value;
      this.taskService.addTask(this.taskForm.value).subscribe(
        (response) => {
          alert('משימה נשמרה בהצלחה');
          this.taskForm.reset();
        },
        (error) => {
          alert('ארעה שגיאה אנא נסה שנית');
        }
      );
    }
  }
}
