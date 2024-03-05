import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModificacionPresupuestoComponent } from './modificacion-presupuesto.component';

describe('ModificacionPresupuestoComponent', () => {
  let component: ModificacionPresupuestoComponent;
  let fixture: ComponentFixture<ModificacionPresupuestoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModificacionPresupuestoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModificacionPresupuestoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
