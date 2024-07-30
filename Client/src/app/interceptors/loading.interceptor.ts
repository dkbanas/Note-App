import {HttpEventType, HttpInterceptorFn} from "@angular/common/http";
import {inject} from "@angular/core";
import {BusyService} from "../services/busy.service";
import {tap} from "rxjs";

let pendingRequests = 0;
export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(BusyService);
  if (pendingRequests === 0) {
    loadingService.showLoading();
  }
  pendingRequests++;

  return next(req).pipe(
    tap({
      next: (event) => {
        // console.log('Received event:', event);
        if (event.type === HttpEventType.Response) {
          pendingRequests--;
          if (pendingRequests === 0) {
            loadingService.hideLoading();
          }
        }
      },
      error: (error) => {
        // console.error('Error occurred:', error);
        pendingRequests--;
        if (pendingRequests === 0) {
          loadingService.hideLoading();
        }
      }
    })
  );
};
