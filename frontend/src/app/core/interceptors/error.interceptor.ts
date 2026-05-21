import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError } from 'rxjs';

export const errorInterceptor:
HttpInterceptorFn = (req, next) => {
  const snack = inject(MatSnackBar);

  return next(req).pipe(catchError((error: HttpErrorResponse) => {
        let message = 'Error inesperado';

        if (error.status === 0) {
          message = 'No se pudo conectar con la API';
        }
        else if (
          error.error?.message
        ) {
          message = error.error.message;
        }
        else if (
          typeof error.error === 'string'
        ) {
          message = error.error;
        }
        else {
          message = `Error ${error.status}`;
        }

        snack.open(message,'Cerrar',
          {
            duration: 4000,
            horizontalPosition:
              'right',
            verticalPosition:
              'top'
          }
        );

        throw error;
      }
    )
  );
};
