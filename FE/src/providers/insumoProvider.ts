import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { Insumo } from "src/interfaces/insumo";

@Injectable()
export class InsumoProvider {

    urlApi: string = environment.urlApi

    constructor(private http: HttpClient) {
    }

    Crear(insumo: Insumo): Observable<any> {
        const url = this.urlApi + 'api/Insumo/Crear';
        const headers = { 'content-type': 'application/json' };
        const body = JSON.stringify(insumo);
        return this.http.post(url, body, { 'headers': headers });
    }

    ObtenerTodos(): Observable<any> {
        const url = this.urlApi + 'api/Insumo/ObtenerTodos';
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }

    ObtenerPorId(id?: any): Observable<Insumo> {
        const url = this.urlApi + 'api/Insumo/ObtenerPorId/?id=' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Insumo>(url, { 'headers': headers })
    }

    Eliminar(id: string): Observable<any> {
        return this.http.put(this.urlApi + 'api/Insumo/Eliminar/?id=' + id, null);
    }

    Editar(insumo: Insumo): Observable<Insumo> {
        const url = this.urlApi + 'api/Insumo/Actualizar';
        return this.http.put<Insumo>(url, insumo);
    }

}
