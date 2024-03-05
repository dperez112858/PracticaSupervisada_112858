import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { Localidad } from "src/interfaces/localidad";

@Injectable()
export class LocalidadProvider {

    urlApi: string = environment.urlApi

    constructor(private http: HttpClient) {
    }

    ObtenerTodas(): Observable<Localidad[]> {
        const url = this.urlApi + 'api/Provincia/ObtenerTodas';
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Localidad[]>(url, { 'headers': headers })
    }

    ObtenerPorId(id: any): Observable<any> {
        const url = this.urlApi + 'api/Localidad/ObtenerPorIdProvincia/?id=' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }
}