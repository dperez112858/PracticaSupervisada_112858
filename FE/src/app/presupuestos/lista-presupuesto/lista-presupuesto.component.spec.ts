import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaPresupuestoComponent } from './lista-presupuesto.component';

describe('ListaPresupuestoComponent', () => {
  let component: ListaPresupuestoComponent;
  let fixture: ComponentFixture<ListaPresupuestoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListaPresupuestoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaPresupuestoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
