import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { API_URL } from '../../../../core/config/api.config';
import { AuthRepository } from '../../domain/ports/auth.repository';
import { Sesion } from '../../domain/models/sesion';
import { LoginResponseDto } from '../dto/login.dto';
import { mapLoginResponse } from '../mappers/auth.mapper';

@Injectable()
export class HttpAuthRepository implements AuthRepository {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = inject(API_URL);

  login(correo: string, contrasena: string): Observable<Sesion> {
    return this.http
      .post<LoginResponseDto>(`${this.apiUrl}/auth`, { correo, contrasena })
      .pipe(map(mapLoginResponse));
  }
}
